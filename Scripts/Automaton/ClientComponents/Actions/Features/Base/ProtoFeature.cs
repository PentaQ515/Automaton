﻿namespace CryoFall.Automaton.ClientComponents.Actions.Features
{
    using AtomicTorch.CBND.CoreMod.Characters;
    using AtomicTorch.CBND.CoreMod.Characters.Player;
    using AtomicTorch.CBND.CoreMod.UI.Controls.Game.Items.Controls;
    using AtomicTorch.CBND.GameApi.Data;
    using AtomicTorch.CBND.GameApi.Data.Characters;
    using AtomicTorch.CBND.GameApi.Data.Items;
    using AtomicTorch.CBND.GameApi.Scripting;
    using AtomicTorch.CBND.GameApi.Scripting.ClientComponents;
    using System.Collections.Generic;

    public abstract class ProtoFeature
    {
        public List<IProtoEntity> EnabledEntityList { get; set; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public List<IProtoEntity> RequiredItemList;

        public bool IsEnabled { get; set; }

        public List<IProtoEntity> EntityList;

        protected IItem SelectedItem => ClientHotbarSelectedItemManager.SelectedItem;

        protected ICharacter CurrentCharacter => Api.Client.Characters.CurrentPlayerCharacter;

        protected PlayerCharacterPrivateState PrivateState => PlayerCharacter.GetPrivateState(CurrentCharacter);

        public void PrepareProto()
        {
            var entityList = new List<IProtoEntity>();
            var requiredItemList = new List<IProtoEntity>();

            this.PrepareFeature(entityList, requiredItemList);

            this.EntityList = entityList;
            this.RequiredItemList = requiredItemList;
        }

        protected abstract void PrepareFeature(List<IProtoEntity> entityList, List<IProtoEntity> requiredItemList);

        /// <summary>
        /// Called by client component every tick.
        /// </summary>
        public virtual void Update(double deltaTime)
        {
            if (!(IsEnabled && CheckPrecondition()))
            {
                return;
            }
            // Check queue and do something.
        }

        /// <summary>
        /// Called by client component on specific time interval.
        /// </summary>
        public virtual void Execute()
        {
            if (!(IsEnabled && CheckPrecondition()))
            {
                return;
            }
            // Check surrounding
            // Add action to queue
        }

        /// <summary>
        /// Check initial condition (right tool equiped or other checkd).
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckPrecondition()
        {
            if (RequiredItemList?.Count == 0)
            {
                return true;
            }
            var selectedProtoItem = SelectedItem?.ProtoItem;
            if (selectedProtoItem != null && RequiredItemList.Contains(selectedProtoItem))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Stop everything.
        /// </summary>
        public virtual void Stop()
        {
        }

        /// <summary>
        /// Init on component enabled.
        /// </summary>
        public virtual void Start(ClientComponent parentComponent)
        {
            SetupSubscriptions(parentComponent);
        }

        /// <summary>
        /// Setup any of subscriptions
        /// </summary>
        public virtual void SetupSubscriptions(ClientComponent parentComponent)
        {
        }
    }
}