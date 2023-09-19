using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SojaExiles
{
    public class LobbyCanvas : MonoBehaviour
    {
        public void ChangeToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}