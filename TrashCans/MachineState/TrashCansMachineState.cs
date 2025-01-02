/* ReSharper disable
 *
 * UnassignedField.Global
 * ClassNeverInstantiated.Global
 * 
 * @TODO: This is Mod Machine State, no above Inspection is required.!
 */

using UnityEngine;
using KSerialization;

namespace SonJeremy.TrashCans.MachineState
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class TrashCansMachineState : StateMachineComponent<TrashCansMachineState.SMInstance>
    {
        private static readonly EventSystem.IntraObjectHandler<TrashCansMachineState>
            OnStorageChange = new EventSystem.IntraObjectHandler<TrashCansMachineState>
            ((GameComponent, GameObject) => GameComponent.OnStorageChanged());

        private static readonly EventSystem.IntraObjectHandler<TrashCansMachineState>
            OnStartWorkable = new EventSystem.IntraObjectHandler<TrashCansMachineState>
            ((GameComponent, GameObject) => GameComponent.PlayWorkable());

        private MeterController StorageMeter;
        
        #pragma warning disable CS0649
        [MyCmpGet] public Storage TrashCansStorage;
        [MyCmpGet] public KAnimControllerBase BaseAminController;
        #pragma warning restore CS0649

        public void PlayWorkable() 
        {
            if (GetComponent<Operational>().IsOperational)
                BaseAminController.Play("working_loop"); 
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();

            StorageMeter = new MeterController(this, Meter.Offset.Infront, Grid.SceneLayer.NoLayer);

            Subscribe((int)GameHashes.OnStorageChange, OnStorageChange);
            Subscribe((int)GameHashes.WorkableStartWork, OnStartWorkable);

            UpdateMeter();
            smi.StartSM();
        }

        private void UpdateMeter() => StorageMeter.SetPositionPercent(Mathf.Clamp01(TrashCansStorage.MassStored() / TrashCansStorage.capacityKg));

        private void OnStorageChanged() => UpdateMeter();

        public class SMInstance : GameStateMachine<States, SMInstance, TrashCansMachineState, object>.GameInstance
        {
            public SMInstance(TrashCansMachineState MasterMachineState) : base(MasterMachineState) {}
        }

        public class States : GameStateMachine<States, SMInstance, TrashCansMachineState>
        {
            public State Off;
            public State Idle;

            public override void InitializeStates(out BaseState DefaultState)
            {
                DefaultState = Off;

                Off
                    .PlayAnim("off")
                    .EventTransition(GameHashes.OperationalChanged, Idle, StateMachineInstance => StateMachineInstance.GetComponent<Operational>().IsOperational);

                Idle
                    .PlayAnim("on")
                    .EventTransition(GameHashes.OperationalChanged, Off, StateMachineInstance => !StateMachineInstance.GetComponent<Operational>().IsOperational);
            }
        }
    }
}
