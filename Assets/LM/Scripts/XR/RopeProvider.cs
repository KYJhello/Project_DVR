using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LM
{
    public class RopeProvider : LocomotionProvider
    {
        [SerializeField] bool allowXMove =  true;
        [SerializeField] bool allowYMove = true;
        [SerializeField] bool allowZMove = true;

        private List<IXRSelectInteractor> grabInteractors = new List<IXRSelectInteractor>();
        private List<RopeInteractable> ropeInteractables = new List<RopeInteractable>();

        Vector3 worldAnchor;
        Vector3 localAnchor;

        protected override void Awake()
        {
            base.Awake();
            
        }

        public void RopeGrab(RopeInteractable interactable, IXRSelectInteractor interactor)
        {
            GameObject ori = system.xrOrigin?.Origin;
            if (ori == null)
                return;

            grabInteractors.Add(interactor);
            ropeInteractables.Add(interactable);

            if (locomotionPhase != LocomotionPhase.Moving)
                locomotionPhase = LocomotionPhase.Started;
        }
        public void RopeOff(IXRSelectInteractor interactor)
        {
            int index = grabInteractors.IndexOf(interactor);
            if (grabInteractors.Count < 1 || ropeInteractables.Count < 1)
                return;
            if (index < 0)
                return;

            if (index > 0 && index == grabInteractors.Count - 1)
            {
                int i = index - 1;
                UpdateAnchor(ropeInteractables[i], grabInteractors[i]);
            }

            grabInteractors.RemoveAt(index);
            ropeInteractables.RemoveAt(index);
        }
        private void UpdateAnchor(RopeInteractable interactable, IXRInteractor interactor)
        {
            Transform newAnchor = interactable.ropes;
            worldAnchor = interactor.transform.position;
            localAnchor = newAnchor.InverseTransformPoint(worldAnchor);
        }

        private void Update()
        {
            if (locomotionPhase == LocomotionPhase.Done)
            {
                locomotionPhase = LocomotionPhase.Idle;
                return;
            }

            if (grabInteractors.Count > 0)
            {
                if (locomotionPhase != LocomotionPhase.Moving)
                {
                    if (!BeginLocomotion())
                        return;

                    locomotionPhase = LocomotionPhase.Moving;
                }

                int lastIndex = grabInteractors.Count - 1;
                IXRSelectInteractor curInteractor = grabInteractors[lastIndex];
                RopeInteractable curRopeInteractable = ropeInteractables[lastIndex];

                if (curInteractor == null || curRopeInteractable == null)
                {
                    EndMove();
                    return;
                }

                RopeMove(curRopeInteractable, curInteractor);
            }
            else if (locomotionPhase != LocomotionPhase.Idle)
                EndMove();
        }

        private void RopeMove(RopeInteractable interactable, IXRSelectInteractor interactor)
        {
            GameObject ori = system.xrOrigin?.Origin;
            if (ori == null)
                return;

            Transform playerPos = ori.transform;
            Vector3 handPos = interactor.transform.position;
            Vector3 moveDir;

            if (allowXMove && allowYMove && allowZMove)
            {
                // No need to check position relative to climbable object if movement is unconstrained
                moveDir = worldAnchor - handPos;
            }
            else
            {
                Transform ropeTransform = interactable.ropes;
                Vector3 interactorRopeLocalPos = ropeTransform.InverseTransformPoint(handPos);
                Vector3 move = localAnchor - interactorRopeLocalPos;

                if (!allowXMove)
                    move.x = 0f;

                if (!allowYMove)
                    move.y = 0f;

                if (!allowZMove)
                    move.z = 0f;

                moveDir = ropeTransform.TransformVector(move);
            }

            playerPos.position += moveDir;
        }
        private void EndMove() 
        {
            EndLocomotion();
            locomotionPhase = LocomotionPhase.Done;
            grabInteractors.Clear();
            ropeInteractables.Clear();
        }
    }
}