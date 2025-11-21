using System;

namespace SonJeremy.TrashCans.AutoConsumption
{
    public class FilterableTrashCans : IUserControlledCapacity
    {
        public Storage Storage;

        public bool WholeValues => true;
        public float MinCapacity => 0.0f;
        public float MaxCapacity => Storage.capacityKg;
        public float AmountStored => Storage.MassStored();
        public LocString CapacityUnits => GameUtil.GetCurrentMassUnit();

        /// <summary>
        /// Added Since U57-700368.
        /// </summary>
        public bool ControlEnabled() => true;

        public float UserMaxCapacity { get => Storage.capacityKg; set { } }

        public object GetUserControlledCapacity() => this;
    }
}