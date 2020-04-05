using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.MountAndBlade;

namespace ClanTweaker.Menu
{
	public abstract class ManagedModOptionData : IOptionData
	{
		protected ManagedModOptionData(ManagedModOptions.ModOptionsType type)
		{
			this.Type = type;
			this._value = ManagedModOptions.GetConfig(type);
		}

		public virtual float GetDefaultValue()
		{
			return 0.0f;
		}

		public readonly ManagedModOptions.ModOptionsType Type;

		private float _value;

		public void Commit()
		{
			throw new NotImplementedException();
		}

		public float GetValue()
		{
			throw new NotImplementedException();
		}

		public void SetValue(float value)
		{
			throw new NotImplementedException();
		}

		public object GetOptionType()
		{
			throw new NotImplementedException();
		}

		public bool IsNative()
		{
			throw new NotImplementedException();
		}
	}
}
