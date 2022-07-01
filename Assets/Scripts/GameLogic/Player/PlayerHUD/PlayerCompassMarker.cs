using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS_Homework_Player
{


    public class PlayerCompassMarker : MonoBehaviour
    {
        public Image MainImage;
        
        public CanvasGroup CanvasGroup;

        public Color DefaultColor;
        
        public Color AltColor;
        
        public bool IsDirection;
        
        public Text TextContent;
        
        public void Initialize(PlayerCompassElement compassElement, string textDirection)
        {
            if (IsDirection && TextContent)
            {
                TextContent.text = textDirection;
            }
            else
            {
                //m_EnemyController = compassElement.transform.GetComponent<EnemyController>();
//
                //if (m_EnemyController)
                //{
                //    m_EnemyController.onDetectedTarget += DetectTarget;
                //    m_EnemyController.onLostTarget += LostTarget;
//
                //    LostTarget();
                //}
            }
        }

        public void DetectTarget()
        {
            MainImage.color = AltColor;
        }

        public void LostTarget()
        {
            MainImage.color = DefaultColor;
        }
    }

}
