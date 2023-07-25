using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamDirChangeTime = 2f;
    private enum EnemeyState
    {
        Roaming
    }

    private EnemeyState enemeyState;
    private EnemyPathFinding enemyPathFinding;
    private void Awake() 
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        enemeyState = EnemeyState.Roaming;
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while(enemeyState == EnemeyState.Roaming)
        {
            Vector2 roamPos = GetRoamingPosition();
            enemyPathFinding.MoveTo(roamPos);
            yield return new WaitForSeconds(roamDirChangeTime);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
