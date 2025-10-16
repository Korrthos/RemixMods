using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Logging;
using Menu.Remix.MixedUI;
using Menu.Remix.MixedUI.ValueTypes;
using UnityEngine;

namespace SplitScreenCoop;

public class SplitScreenCoopOptions : OptionInterface
{
    class BetterComboBox : OpComboBox
    {
        public BetterComboBox(ConfigurableBase configBase, Vector2 pos, float width, List<ListItem> list) : base(configBase, pos, width, list) { }
        public override void GrafUpdate(float timeStacker)
        {
            base.GrafUpdate(timeStacker);
            if(this._rectList != null && !_rectList.isHidden)
            {
                for (int j = 0; j < 9; j++)
                {
                    this._rectList.sprites[j].alpha = 1;
                }
            }
        }
    }
    public SplitScreenCoopOptions()
    {
        PreferredSplitMode = this.config.Bind("PreferredSplitMode", SplitScreenCoop.SplitMode.SplitVertical);
        AlwaysSplit = this.config.Bind("AlwaysSplit", false);
        AllowCameraSwapping = this.config.Bind("AllowCameraSwapping", false);
        DualDisplays = this.config.Bind("DualDisplays", false);
        TripleDisplays = this.config.Bind("TripleDisplays", false);
        QuadDisplays = this.config.Bind("QuadDisplays", false);
    }

    public readonly Configurable<SplitScreenCoop.SplitMode> PreferredSplitMode;
    public readonly Configurable<bool> AlwaysSplit;
    public readonly Configurable<bool> AllowCameraSwapping;
    public readonly Configurable<bool> DualDisplays;
    public readonly Configurable<bool> TripleDisplays;
    public readonly Configurable<bool> QuadDisplays;
    private UIelement[] UIArrOptions;

    public override void Initialize()
    {
        var opTab = new OpTab(this, "Options");
        this.Tabs = new[] { opTab };
        OpCheckBox e1;
        OpCheckBox e2;
        OpCheckBox e3;
        UIArrOptions = new UIelement[]
        {
            new OpLabel(10f, 550f, "General", true),

            new OpCheckBox(AlwaysSplit, 10f, 450),
            new OpLabel(40f, 450, "Permanent split mode") { verticalAlignment = OpLabel.LabelVAlignment.Center },

            new OpCheckBox(AllowCameraSwapping, 10f, 410),
            new OpLabel(40f, 410, "Allow camera swapping even if there's enough cameras") { verticalAlignment = OpLabel.LabelVAlignment.Center },

            e1 = new OpCheckBox(DualDisplays, 10f, 370) { description = "Requires two physical displays" },
            new OpLabel(40f, 370, "Dual Display (experimental)") { verticalAlignment = OpLabel.LabelVAlignment.Center },

            e2 = new OpCheckBox(TripleDisplays, 10f, 330) { description = "Requires three physical displays" },
            new OpLabel(40f, 330, "Triple Displays (experimental)") { verticalAlignment = OpLabel.LabelVAlignment.Center },

            e3 = new OpCheckBox(QuadDisplays, 10f, 290) { description = "Requires four physical displays" },
            new OpLabel(40f, 290, "Quadruple Displays (experimental)") { verticalAlignment = OpLabel.LabelVAlignment.Center },


            // added last due to overlap
            new OpLabel(10f, 520, "Split Mode") { verticalAlignment = OpLabel.LabelVAlignment.Center },
            new BetterComboBox(PreferredSplitMode, new Vector2(10f, 490), 200f, OpResourceSelector.GetEnumNames(null, typeof(SplitScreenCoop.SplitMode)).ToList()),
        };

        e1.greyedOut = !SplitScreenCoop.MultipleDisplaysSupported(2);
        e2.greyedOut = !SplitScreenCoop.MultipleDisplaysSupported(3);
        e3.greyedOut = !SplitScreenCoop.MultipleDisplaysSupported(4);

        // Add items to the tab
        opTab.AddItems(UIArrOptions);
    }
}
