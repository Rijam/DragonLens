﻿using DragonLens.Core.Loaders.UILoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace DragonLens.Content.GUI
{
	public class Tooltip : SmartUIState, ILoadable
	{
		private static string text = string.Empty;
		private static string tooltip = string.Empty;

		public override bool Visible => true;

		public void Load(Mod mod)
		{
			On.Terraria.Main.Update += Reset;
		}

		public override int InsertionIndex(List<GameInterfaceLayer> layers)
		{
			return layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text")) + 1;
		}

		public static void SetName(string name)
		{
			text = name;
		}

		public static void SetTooltip(string newTooltip)
		{
			ReLogic.Graphics.DynamicSpriteFont font = Terraria.GameContent.FontAssets.MouseText.Value;
			tooltip = Helpers.GUIHelper.WrapString(newTooltip, 200, font, 1);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (text == string.Empty)
				return;

			ReLogic.Graphics.DynamicSpriteFont font = Terraria.GameContent.FontAssets.MouseText.Value;

			float nameWidth = ChatManager.GetStringSize(font, text, Vector2.One).X;
			float tipWidth = ChatManager.GetStringSize(font, tooltip, Vector2.One).X * 0.9f;

			float width = Math.Max(nameWidth, tipWidth);
			float height = -16;
			Vector2 pos;

			if (Main.MouseScreen.X > Main.screenWidth - width)
			{
				width = Math.Max(nameWidth, tipWidth);
				pos = Main.MouseScreen - new Vector2(width + 20, 0);
			}
			else
			{
				width = Math.Max(nameWidth, tipWidth);
				pos = Main.MouseScreen + new Vector2(40, 0);
			}

			height += ChatManager.GetStringSize(font, "{Dummy}\n" + tooltip, Vector2.One).Y + 16;

			Utils.DrawInvBG(Main.spriteBatch, new Rectangle((int)pos.X - 10, (int)pos.Y - 10, (int)width + 20, (int)height + 20), new Color(20, 20, 55) * 0.925f);

			Utils.DrawBorderString(Main.spriteBatch, text, pos, Color.White);
			pos.Y += ChatManager.GetStringSize(font, text, Vector2.One).Y + 4;

			Utils.DrawBorderString(Main.spriteBatch, tooltip, pos, Color.LightGray, 0.9f);
		}

		private void Reset(On.Terraria.Main.orig_Update orig, Main self, GameTime gameTime)
		{
			orig(self, gameTime);

			//reset
			text = string.Empty;
			tooltip = string.Empty;
		}
	}
}