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

        //•	If the payment is for a book, create a duplicate packing slip for the royalty department.
        [TestMethod]
        public void BookGenerateDuplicatePackingSlip()
        {
            PackingSlip expectedPackingSlip = new PackingSlip("Physical_Product_Name",       
                "SOURCE", "DESTINATION",        
                "HR", new Agent("Smith", true));
            BookPayment bookPayment = new BookPayment();
            if (bookPayment.GeneratePackingSlip(expectedPackingSlip.Name,
                expectedPackingSlip.Source,
                expectedPackingSlip.Destination,
                expectedPackingSlip.Department,
                expectedPackingSlip.CommissionAgent.Name))
            {
                Assert.AreEqual(2, bookPayment.PackingSlips.Count);
                Assert.AreEqual(bookPayment.PackingSlips.First(), 
                    bookPayment.PackingSlips.Last());
                Assert.AreEqual("royalty", bookPayment.PackingSlips.Last().Department);             
            }
            else
            {
                Assert.Fail("No packing slips exist!");
            }
        }

        //•	If the payment is for a membership, activate that membership.
        [TestMethod]
        public void MembershipActivateMembership()
        {
            MembershipPayment membership = new MembershipPayment();
            Owner owner = new Owner("SMITH", "SMITH@oc.com");
            if (membership.ActivateMembership(owner.Name, owner.Email))  
            {
                if (membership.Owners.Any())
                {
                    Assert.AreEqual(membership.Owners.First().Name, owner.Name);
                    // Check this is now a member.
                    Assert.AreEqual(membership.Owners.First().IsMember, true);
                }
                else
                {
                    Assert.Fail("Failed to add a member!");
                }    
            }
        }

        //••	If the payment is an upgrade to a membership, apply the upgrade.
        [TestMethod]
        public void MembershipUpgradeMembership()
        {
            MembershipPayment membership = new MembershipPayment();
            Owner owner = new Owner("SMITH", "SMITH@oc.com");
            if (membership.UpgradeMembership(owner.Email))
            {
                if (membership.Owners.Any())
                {
                    Assert.AreEqual(membership.Owners.First().Name, owner.Name);
                    // Check this is now a member.
                    Assert.AreEqual(membership.Owners.First().IsMember, true);
                    // Check this member is upgraded.
                    Assert.AreEqual(membership.Owners.First().IsUpgraded, true);
                }
                else
                {
                    Assert.Fail("Failed to add a member!");
                }
            }
        }

        // •	If the payment is for the video “Learning to Ski,” add a free “First Aid” video to the packing slip (the result of a court decision in 1997).
        [TestMethod]
        public void VideoLearningToSkiPayment()
        {
            PackingSlip packingSlip = new PackingSlip("Learning to Ski", "source", 
                "destination", "department", new Video());
            VideoPayment videoPayment = new VideoPayment();

            if (videoPayment.MakePayment(packingSlip.Name, packingSlip.Source,
                packingSlip.Destination, packingSlip.Department,
                packingSlip.CommissionAgent.Name))
            {
                // Check that packing slips actually exist.
                Assert.AreEqual(videoPayment.PackingSlips.Count > 0, true);
                // Make sure the First Aid video got added.
                Assert.AreEqual(videoPayment.PackingSlips.
                    SelectMany(p => p.AddedProducts).Any(p => p.Name == "First Aid"), true);
            }
        }
    }
}
