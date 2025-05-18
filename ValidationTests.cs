using System;
using System.Collections.Generic;
using Xunit;
using Console_test_task.Models;

public class ValidationTests
{
    [Fact]
    public void Box_ShouldNotExceedPalletWidthOrDepth()
    {
        var pallet = new Pallet
        {
            Width = 100,
            Depth = 100,
        };

        var box = new Box
        {
            Width = 101, // превышает
            Depth = 50
        };

        bool isValid = IsBoxFitInPallet(box, pallet);

        Assert.False(isValid, " оробка по ширине превышает паллету, должно быть False");
    }

    [Fact]
    public void Box_ShouldBeInvalid_IfNoExpirationAndNoProductionDate()
    {
        var box = new Box
        {
            ExpirationDate = null,
            ProductionDate = null
        };

        bool isValid = IsBoxValid(box);

        Assert.False(isValid, " оробка без срока годности и даты производства должна быть невалидной");
    }

    [Fact]
    public void Pallet_ShouldNotAllowNegativeDimensions()
    {
        var pallet = new Pallet
        {
            Width = -1,
            Height = 120,
            Depth = 100
        };

        bool isValid = IsPalletValid(pallet);

        Assert.False(isValid, "ѕаллет с отрицательной шириной должен быть невалидным");
    }

    private bool IsBoxFitInPallet(Box box, Pallet pallet)
    {
        return box.Width <= pallet.Width && box.Depth <= pallet.Depth;
    }

    private bool IsBoxValid(Box box)
    {
        return box.ExpirationDate != null || box.ProductionDate != null;
    }

    private bool IsPalletValid(Pallet pallet)
    {
        return pallet.Width >= 0 && pallet.Height >= 0 && pallet.Depth >= 0;
    }
}
