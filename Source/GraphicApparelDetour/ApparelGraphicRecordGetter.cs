using RimWorld;
using UnityEngine;
using Verse;

namespace GraphicApparelDetour
{
    // Token: 0x02000003 RID: 3
    internal static class ApparelGraphicRecordGetter
    {
        // Token: 0x06000003 RID: 3 RVA: 0x00002294 File Offset: 0x00000494
        internal static bool TryGetGraphicApparel(Apparel apparel, BodyTypeDef bodyType, out ApparelGraphicRecord rec)
        {
            var drawColor = apparel.DrawColor;
            var white = Color.white;
            if (bodyType == null)
            {
                Log.Error("Getting apparel graphic with undefined body type.");
                bodyType = BodyTypeDefOf.Male;
            }

            bool result;
            if (apparel.def.apparel.wornGraphicPath.NullOrEmpty())
            {
                rec = new ApparelGraphicRecord(null, null);
                result = false;
            }
            else
            {
                var shader = ShaderDatabase.Cutout;
                if (apparel.def.graphicData.shaderType == ShaderTypeDefOf.CutoutComplex)
                {
                    shader = ShaderDatabase.CutoutComplex;
                }

                string text;
                if (apparel.def.apparel.LastLayer == ApparelLayerDefOf.Overhead)
                {
                    text = apparel.def.apparel.wornGraphicPath;
                }
                else
                {
                    text = apparel.def.apparel.wornGraphicPath + "_" + bodyType;
                }

                var graphic = GraphicDatabase.Get<Graphic_Multi>(text, shader, apparel.def.graphicData.drawSize,
                    drawColor, white);
                rec = new ApparelGraphicRecord(graphic, apparel);
                result = true;
            }

            return result;
        }
    }
}