using UnityEngine;

public class PlayerOnTrainRigidbody : MonoBehaviour
{
    public TrainMovement trainScript;
    public TrainAudioController trainAudioController;

    private Rigidbody rb;
    private Vector3 lastTrainPos;
    private bool onTrain = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastTrainPos = trainScript.transform.position;
    }

    void FixedUpdate()
    {
        if (onTrain)
        {
            Vector3 trainMovement = trainScript.transform.position - lastTrainPos;
            rb.MovePosition(rb.position + trainMovement);
        }

        lastTrainPos = trainScript.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject == trainScript.gameObject || collision.transform.IsChildOf(trainScript.transform)) && !onTrain)
        {
            // Only trigger if not already on train
            onTrain = true;
            lastTrainPos = trainScript.transform.position;

            // Start train movement
            if (!trainScript.startMoving)
            {
                trainScript.startMoving = true;
                Debug.Log("🚆 Train started moving.");
            }

            // Play audio only once
            if (trainAudioController != null)
            {
                trainAudioController.PlayAudio();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == trainScript.gameObject || collision.transform.IsChildOf(trainScript.transform))
        {
            onTrain = false;
            Debug.Log("🧍 Player left the train.");

            if (trainAudioController != null)
            {
                trainAudioController.StopAudio();
            }
        }
    }
}
