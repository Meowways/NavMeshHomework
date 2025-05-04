using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _camera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed = 2;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _camera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed = 0;
        }
    }
}
