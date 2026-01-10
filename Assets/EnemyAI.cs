// using UnityEngine;

// // Ten skrypt steruje zachowaniem przeciwnika.
// // Wróg potrafi: patrolować, gonić gracza i atakować.

// public class EnemyAI : MonoBehaviour
// {
//     public enum State { Patrol, Chase, Attack, ReturnHome }
//     [Header("Movement")]
//     // public float samePlatformYTolerance = 0.6f;
//     public float ignorePlayerIfBelowY = 1.2f;
//     public float speed = 2f;          // prędkość poruszania się przeciwnika
//     public float chaseDistance = 4f;  // odległość, od której wróg zaczyna gonić gracza
//     public float attackDistance = 1f; // odległość, od której wróg atakuje

//     [Header("Patrol")]
//     public Transform[] patrolPoints;  // punkty patrolu (puste obiekty w scenie)
//     private int currentPoint = 0;     // aktualny punkt patrolu

//     [Header("References")]
//     public Transform player;          // obiekt gracza (przeciągnąć w Inspectorze)
//     public Animator anim;             // animator przeciwnika

//     [Header("Attack")]
//     public int damage = 10;           // obrażenia zadawane graczowi
//     public float attackCooldown = 1.5f; // czas pomiędzy atakami
//     private float lastAttackTime;     // kiedy był ostatni atak
//     private State state = State.Patrol;
//     private float lockedY;

//     private void Start()
//     {
//         lockedY = transform.position.y;
//         if (patrolPoints == null) patrolPoints = new Transform[0];
//     }

//     void Update()
//     {
//         if (player == null || patrolPoints.Length < 2)
//             return;

//         // Игрок слишком низко (1-й этаж) -> враг не реагирует
//         bool playerTooLow = player.position.y < lockedY - ignorePlayerIfBelowY;

//         // Дистанция по X (через Vector2.Distance, но без влияния прыжков)
//         float distance = Vector2.Distance(
//             new Vector2(transform.position.x, lockedY),
//             new Vector2(player.position.x, lockedY)
//         );

//         if (playerTooLow)
//         {
//             if (state == State.Chase || state == State.Attack)
//                 state = State.ReturnHome;
//             else
//                 state = State.Patrol;
//         }
//         else
//         {
//             if (distance <= attackDistance)
//                 state = State.Attack;
//             else if (distance <= chaseDistance)
//                 state = State.Chase;
//             else
//             {
//                 if (state == State.Chase || state == State.Attack)
//                     state = State.ReturnHome;
//                 else
//                     state = State.Patrol;
//             }
//         }

//         switch (state)
//         {
//             case State.Patrol: Patrol(); break;
//             case State.Chase: Chase(); break;
//             case State.Attack: Attack(); break;
//             case State.ReturnHome: ReturnHome(); break;
//         }
//     }

//     // ---------------- PATROL ----------------
//     void Patrol()
//     {
//         // Włączamy animację ruchu
//         if (anim!=null) anim.SetBool("IsMoving", true);

//         // Aktualny punkt patrolu
//         Transform target = patrolPoints[currentPoint];
//         Vector2 targetPosition = new Vector2(target.position.x, lockedY);

//         // Ruch w kierunku punktu patrolu
//         transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);


//         // Jeśli dotarliśmy do punktu → zmień punkt
//         if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
//         {
//             currentPoint = (currentPoint + 1) % patrolPoints.Length;
//         }

//         // Obrót sprite’a w stronę ruchu
//         Flip(target.position.x - transform.position.x);
//     }

//     // ---------------- CHASE ----------------
//     void Chase()
//     {
//         if (anim!=null) anim.SetBool("IsMoving", true);

//         Vector2 targetPosition = new Vector2(player.position.x, lockedY);

//         // Ruch w stronę gracza
//         transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

//         Flip(player.position.x - transform.position.x);
//     }

//     // ---------------- RETURN HOME ----------------
//     void ReturnHome()
//     {
//         if (anim != null) anim.SetBool("IsMoving", true);

//         Transform home = patrolPoints[0];
//         Vector2 homePosition = new Vector2(home.position.x, lockedY);

//         transform.position = Vector2.MoveTowards(transform.position, homePosition, speed * Time.deltaTime);
//         Flip(home.position.x - transform.position.x);

//         if (Vector2.Distance(transform.position, homePosition) < 0.1f)
//         {
//             currentPoint = 1;
//             state = State.Patrol;
//         }
//     }

//     // ---------------- ATTACK ----------------
//     void Attack()
//     {
//         // Cooldown – żeby wróg nie atakował co klatkę
//         if (Time.time < lastAttackTime + attackCooldown)
//             return;

//         // Trigger animacji ataku
//         if (anim!=null) anim.SetTrigger("Attack");

//         // Zadawanie obrażeń graczowi
//         PlayerStats stats = player.GetComponent<PlayerStats>();
//         if (stats != null)
//         {
//             stats.TakeDamage(damage);
//         }

//         lastAttackTime = Time.time;
//     }
//     /// <summary>
//     /// /////////////////////////////////////////////////////////////////////////////////////////////
//     /// </summary>
//     /// <param name="directionX"></param>
//     // ---------------- FLIP ----------------
//     void Flip(float directionX)
//     {
//         // Jeśli różnica jest bardzo mała – nie obracamy sprite’a
//         if (Mathf.Abs(directionX) < 0.01f) return;

//         // Obrót sprite’a w lewo / prawo
//         transform.localScale = new Vector3(
//             Mathf.Sign(directionX),
//             1,
//             1
//         );
//     }
// }

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack, ReturnHome }

    [Header("Movement")]
    public float ignorePlayerIfBelowY = 1.2f;
    public float speed = 2f;
    public float chaseDistance = 4f;
    public float loseChaseDistance = 6f;
    public float attackDistance = 1f;

    [Header("Attack Height Filter")]
    public float attackYTolerance = 0.35f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    [Header("References")]
    public Transform player;
    public Animator anim;

    [Header("Attack")]
    public int damage = 10;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    private State state = State.Patrol;
    private float lockedY;
    private float lastMoveDirX = 1f;

    private void Start()
    {
        lockedY = transform.position.y;
        if (patrolPoints == null) patrolPoints = new Transform[0];
    }

    void Update()
    {
        if (player == null || patrolPoints.Length < 2) return;

        float minX = Mathf.Min(patrolPoints[0].position.x, patrolPoints[1].position.x);
        float maxX = Mathf.Max(patrolPoints[0].position.x, patrolPoints[1].position.x);

        bool playerTooLow = player.position.y < lockedY - ignorePlayerIfBelowY;

        bool playerOutsideX = (player.position.x < minX) || (player.position.x > maxX);

        float distanceX = Vector2.Distance(
            new Vector2(transform.position.x, lockedY),
            new Vector2(player.position.x, lockedY)
        );

        float yDiff = Mathf.Abs(player.position.y - lockedY);

        if (playerTooLow || playerOutsideX)
        {
            if (state == State.Chase || state == State.Attack)
                state = State.ReturnHome;
            else if (state == State.ReturnHome)
                state = State.ReturnHome;
            else
                state = State.Patrol;
        }
        else
        {

            if (state == State.Patrol)
            {
                if (distanceX <= chaseDistance)
                    state = State.Chase;
            }
            else if (state == State.Chase)
            {
                if (distanceX > loseChaseDistance)
                {
                    state = State.Patrol;
                    currentPoint = (lastMoveDirX < 0f) ? 0 : 1;
                }
                else if (distanceX <= attackDistance && yDiff <= attackYTolerance)
                {
                    state = State.Attack;
                }
                else
                {
                    state = State.Chase;
                }
            }
            else if (state == State.Attack)
            {
                if (distanceX > attackDistance || yDiff > attackYTolerance)
                {
                    if (distanceX > loseChaseDistance)
                    {
                        state = State.Patrol;
                        currentPoint = (lastMoveDirX < 0f) ? 0 : 1;
                    }
                    else
                    {
                        state = State.Chase;
                    }
                }
            }
            else if (state == State.ReturnHome)
            {
                if (distanceX <= chaseDistance)
                    state = State.Chase;
            }
        }

        switch (state)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: Chase(minX, maxX); break;
            case State.Attack: Attack(); break;
            case State.ReturnHome: ReturnHome(minX); break;
        }
    }

    // ---------------- PATROL ----------------
    void Patrol()
    {
        if (anim != null) anim.SetBool("IsMoving", true);

        Transform target = patrolPoints[currentPoint];
        Vector2 targetPos = new Vector2(target.position.x, lockedY);

        MoveTowards(targetPos);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }

    // ---------------- CHASE ----------------
    void Chase(float minX, float maxX)
    {
        if (anim != null) anim.SetBool("IsMoving", true);

        // Игрок внутри рамки, но clamp безопасен
        float clampedX = Mathf.Clamp(player.position.x, minX, maxX);
        Vector2 targetPos = new Vector2(clampedX, lockedY);

        MoveTowards(targetPos);
    }

    // ---------------- RETURN HOME ----------------
    void ReturnHome(float minX)
    {
        if (anim != null) anim.SetBool("IsMoving", true);

        Vector2 homePos = new Vector2(minX, lockedY);
        MoveTowards(homePos);

        if (Vector2.Distance(transform.position, homePos) < 0.1f)
        {
            currentPoint = 1;
            state = State.Patrol;
        }
    }

    // ---------------- ATTACK ----------------
    void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        if (anim != null) anim.SetTrigger("Attack");

        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null) stats.TakeDamage(damage);

        lastAttackTime = Time.time;
    }

    // ---------------- MOVE + FLIP ----------------
    void MoveTowards(Vector2 targetPos)
    {
        Vector2 prev = transform.position;

        transform.position = Vector2.MoveTowards(prev, targetPos, speed * Time.deltaTime);

        float dx = targetPos.x - prev.x;
        if (Mathf.Abs(dx) > 0.001f)
        {
            lastMoveDirX = Mathf.Sign(dx);
            Flip(dx);
        }
    }

    void Flip(float directionX)
    {
        if (Mathf.Abs(directionX) < 0.01f) return;
        transform.localScale = new Vector3(Mathf.Sign(directionX), 1, 1);
    }
}
