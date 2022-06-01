using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace FPS_Homework_Enemy
{
    public class EnemyUI : MonoBehaviour
    {
        
        public EnemyEntityBase EnemyEntity;
        public Transform HealthBarPivot;
        public Image HealthBarImage;

        public void InitUI()
        {
            EnemyEntity = GetComponent<EnemyEntityBase>();
        }

        public void OnUpdateEnemyUI()
        {
            LookAtPlayer();
            HealthBarImage.fillAmount = 
                EnemyEntity.EntityCurrentHealth / EnemyEntity.EntityTotalTotalHealth;
        }


        private void LookAtPlayer()
        {
            HealthBarPivot.LookAt(Camera.main.transform.position);
        }
        
    }

}
