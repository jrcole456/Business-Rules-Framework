using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BusinessRulesEngine;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestBussinessRule
    {
        [TestMethod]
        // If the payment is for a physical product, generate a packing slip for shipping.
        public void PhysicalProductGeneratePackingSlipTest()
        {
            PackingSlip expectedPackingSlip = new PackingSlip("Physical_Product_Name", 
                "SOURCE", "DESTINATION",                 
                "HR", new Agent("Smith", true));

            PhysicalProductPayment physicalProductPayment = new PhysicalProductPayment();
            if (physicalProductPayment.GeneratePackingSlip(expectedPackingSlip.Name,
                expectedPackingSlip.Source,
                expectedPackingSlip.Destination,
                expectedPackingSlip.Department,
                expectedPackingSlip.CommissionAgent.Name))
            {
                if (physicalProductPayment.PackingSlips.Any())
                {
                    Assert.AreEqual(expectedPackingSlip, physicalProductPayment.PackingSlips[0]);
                }
                else
                {
                    Assert.Fail("No packing slips exist!");
                }
            }
            else
            {
                Assert.Fail("Failed to generate packing slip!");
            }
        }
    }
}
