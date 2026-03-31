//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace ClockSample
//{

//	public class Clock : MonoBehaviour
//	{
//		public Transform handHours;
//		public Transform handMinutes;
//		public Transform handSeconds;

//		private void Start()
//		{
//			//we are only updating everything once a second since this is sufficient for our clock
//			InvokeRepeating(nameof(UpdateHands), 0, 1);
//		}

//		void UpdateHands()
//		{
//			//get the current time and convert it to hand rotation
//			float handRotationHours		= System.DateTime.Now.Hour		* 30;	//360/12 = 30
//			float handRotationMinutes	= System.DateTime.Now.Minute	* 6;	//360/60 = 6
//			float handRotationSeconds	= System.DateTime.Now.Second	* 6;	//360/60 = 6

//			//create vectors that we can assign to the transforms
//			Vector3 hoursVec	= new Vector3(0, 0, handRotationHours);
//			Vector3 minutesVec	= new Vector3(0, 0, handRotationMinutes);
//			Vector3 secondsVec	= new Vector3(0, 0, handRotationSeconds);

//			//assign the rotation to the hand Transforms
//			if (handHours)
//			{
//				handHours.localEulerAngles = hoursVec;
//			}

//			if (handMinutes)
//			{
//				handMinutes.localEulerAngles = minutesVec;
//			}

//			if (handSeconds)
//			{
//				handSeconds.localEulerAngles = secondsVec;
//			}
//		}

//		private void OnDestroy()
//		{
//			CancelInvoke();
//		}
//	}

//}
using System;
using UnityEngine;

namespace ClockSample
{
    public class Clock : MonoBehaviour
    {
        public Transform handHours;
        public Transform handMinutes;
        public Transform handSeconds;

        [Header("Heure de départ personnalisée")]
        public int startHour = 8;   // Heure de début
        public int startMinute = 0; // Minute de début
        public int startSecond = 0; // Seconde de début

        private DateTime startTime;

        private void Start()
        {
            // Fixer le point de départ à 8:00:00 (ou selon les valeurs dans l'inspecteur)
            startTime = new DateTime(1, 1, 1, startHour, startMinute, startSecond);

            // Mise à jour toutes les secondes
            InvokeRepeating(nameof(UpdateHands), 0, 1);
        }

        void UpdateHands()
        {
            // Temps écoulé depuis le lancement
            TimeSpan elapsed = DateTime.Now - DateTime.Today; // temps système depuis minuit
            elapsed = TimeSpan.FromSeconds(Time.time);        // temps écoulé dans Unity

            // On ajoute le temps écoulé à l'heure de départ
            DateTime currentTime = startTime.AddSeconds(elapsed.TotalSeconds);

            // Conversion en rotation
            float handRotationHours = currentTime.Hour * 30f; // 360/12
            float handRotationMinutes = currentTime.Minute * 6f;  // 360/60
            float handRotationSeconds = currentTime.Second * 6f;  // 360/60

            if (handHours)
                handHours.localEulerAngles = new Vector3(0, 0, handRotationHours);
            if (handMinutes)
                handMinutes.localEulerAngles = new Vector3(0, 0, handRotationMinutes);
            if (handSeconds)
                handSeconds.localEulerAngles = new Vector3(0, 0, handRotationSeconds);
        }

        private void OnDestroy()
        {
            CancelInvoke();
        }
    }
}

//using System;
//using UnityEngine;

//public class ExamSimulationManager : MonoBehaviour
//{
//    [Header("Horloge")]
//    public Transform handHours;
//    public Transform handMinutes;
//    public Transform handSeconds;

//    [Header("Étudiants & Animations")]
//    public GameObject[] students;          // tous les étudiants
//    public Animator[] studentAnimators;    // leurs animators (triche, pleurs, etc.)

//    [Header("Configuration Temps")]
//    public int startHour = 8;
//    public int startMinute = 0;
//    public int startSecond = 0;
//    public float simulationDurationMinutes = 20f; // après 20 min tout s'arrête

//    private DateTime startTime;
//    private float elapsedTime = 0f;
//    private bool simulationEnded = false;

//    void Start()
//    {
//        // Horloge démarre à 8h00
//        startTime = new DateTime(1, 1, 1, startHour, startMinute, startSecond);

//        // Mise à jour chaque seconde
//        InvokeRepeating(nameof(UpdateClock), 0, 1);
//    }

//    void UpdateClock()
//    {
//        if (simulationEnded) return;

//        elapsedTime += 1f;

//        // Vérifier si temps écoulé > 20 min
//        if (elapsedTime >= simulationDurationMinutes * 60f)
//        {
//            EndSimulation();
//            return;
//        }

//        // Avancer le temps
//        DateTime currentTime = startTime.AddSeconds(elapsedTime);

//        // Rotation des aiguilles
//        if (handHours)
//            handHours.localEulerAngles = new Vector3(0, 0, currentTime.Hour * 30f);
//        if (handMinutes)
//            handMinutes.localEulerAngles = new Vector3(0, 0, currentTime.Minute * 6f);
//        if (handSeconds)
//            handSeconds.localEulerAngles = new Vector3(0, 0, currentTime.Second * 6f);
//    }

//    void EndSimulation()
//    {
//        simulationEnded = true;
//        CancelInvoke(nameof(UpdateClock));

//        Debug.Log("? Fin de l’examen après 20 minutes !");

//        // Désactiver étudiants
//        foreach (var student in students)
//        {
//            if (student != null) student.SetActive(false);
//        }

//        // Arrêter leurs animations
//        foreach (var anim in studentAnimators)
//        {
//            if (anim != null) anim.enabled = false;
//        }

//        // Désactiver aussi l’horloge si tu veux
//        if (handHours) handHours.gameObject.SetActive(false);
//        if (handMinutes) handMinutes.gameObject.SetActive(false);
//        if (handSeconds) handSeconds.gameObject.SetActive(false);

//        // ?? Ici tu peux ajouter :
//        // - afficher un écran "Fin d’examen"
//        // - jouer un son de cloche
//        // - charger une nouvelle scène
//    }
//}
