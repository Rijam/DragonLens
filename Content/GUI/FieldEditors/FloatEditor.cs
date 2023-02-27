﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DragonLens.Content.GUI.FieldEditors
{
	internal class FloatEditor : FieldEditor
	{
		public TextField entry;

		public FloatEditor(string name, Action<object> onValueChanged, float initialValue) : base(70, name, onValueChanged, initialValue)
		{
			entry = new(InputType.number);
			entry.Left.Set(10, 0);
			entry.Top.Set(32, 0);
			entry.currentValue = initialValue.ToString();
			Append(entry);
		}

		public override void Update(GameTime gameTime)
		{
			if (entry.updated)
				onValueChanged(float.TryParse(entry.currentValue, out float value) ? value : 0);

			base.Update(gameTime);
		}

		public override void SafeDraw(SpriteBatch sprite)
		{
			base.SafeDraw(sprite);
		}
	}
}