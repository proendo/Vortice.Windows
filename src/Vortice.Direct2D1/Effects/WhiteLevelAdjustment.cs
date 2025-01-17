﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class WhiteLevelAdjustment : ID2D1Effect
    {
        public WhiteLevelAdjustment(ID2D1DeviceContext context)
             : base(context.CreateEffect(EffectGuids.WhiteLevelAdjustment))
        {
        }

        public WhiteLevelAdjustment(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.WhiteLevelAdjustment))
        {
        }

        public float InputWhiteLebel
        {
            get => GetFloatValue((int)WhiteLevelAdjustmentProperties.InputWhiteLevel);
            set => SetValue((int)WhiteLevelAdjustmentProperties.InputWhiteLevel, value);
        }

        public float OutputWhiteLevel
        {
            get => GetFloatValue((int)WhiteLevelAdjustmentProperties.OutputWhiteLevel);
            set => SetValue((int)WhiteLevelAdjustmentProperties.OutputWhiteLevel, value);
        }
    }
}
