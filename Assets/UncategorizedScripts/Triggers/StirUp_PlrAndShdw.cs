using UnityEngine;

public class StirUp_PlrAndShdw : MonoBehaviour
{
    public StateMachine stateMachine;
    public KnightController knightController;
    public ShadowStateMachine shadowStateMachine;
    public ShadowController shadowController;
    void Start()
    {
        stateMachine.enabled = false;
        knightController.enabled = false;
        shadowStateMachine.enabled = false;
        shadowController.enabled = false;
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        stateMachine.enabled = true;
        knightController.enabled = true;
        shadowStateMachine.enabled = true;
        shadowController.enabled = true;
    }
}
