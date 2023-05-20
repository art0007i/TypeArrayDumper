using HarmonyLib;
using NeosModLoader;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrooxEngine;
using BaseX;
using System.Text;

namespace TypeArrayDumper
{
    public class TypeArrayDumper : NeosMod
    {
        public override string Name => "TypeArrayDumper";
        public override string Author => "art0007i";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/art0007i/TypeArrayDumper/";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.art0007i.TypeArrayDumper");
            harmony.PatchAll();

        }
        [HarmonyPatch(typeof(WorkerManager), "NewIndexes")]
        class TypeArrayDumperPatch
        {
            public static void Postfix(SyncArray<string> sender, int startIndex, int length)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    int num = startIndex + i;
                    if (num != 0)
                    {
                        string text = sender[num];
                        Type type = WorkerManager.GetType(text);
                        sb.AppendLine(string.Format($"Adding type {text} at index {num} (deserialized as {((type == null) ? "NULLTYPE" : type.FullName)}) in world {sender.World.Name}"));
                    }
                }
                Msg(sb.ToString().TrimEnd());
            }
        }

    }
}