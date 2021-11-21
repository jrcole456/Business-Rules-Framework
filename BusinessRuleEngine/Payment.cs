using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRulesEngine
{    
    /// <summary>
    /// Holds the product name.
    /// </summary>
    public abstract class Product
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defualt constructor.
        /// </summary>
        public Product()
        {
            Name = String.Empty;
        }
    }

    /// <summary>
    /// We must implement all these required mathods for our payment system.
    /// </summary>
    interface IPayment
    {
        /// <summary>
        /// This generates the packing slip -  adds the packing slip to already existing packing
        /// slips that need to be processed.
        /// </summary>
        /// <param name="packingName">The actual packing slip name - there could be duplicates so we have the unique key identifier.</param>
        /// <param name="packingSource"></param>
        /// <param name="packingDestination"></param>
        /// <param name="department">The department of the packing slip i.e. Royalty department.</param>
        /// <param name="agentName">The name of the agent who requested the commision payment.</param>
        /// <returns></returns>
        bool GeneratePackingSlip(string packingName, string packingSource, string packingDestination, 
            string department, string agentName);

        /// <summary>
        /// This make the payment 
        /// </summary>
        /// <param name="packingName"></param>
        /// <param name="packingSource"></param>
        /// <param name="packingDestination"></param>
        /// <param name="department"></param>
        /// <param name="agentName"></param>
        /// <returns></returns>
        bool MakePayment(string packingName, string packingSource, string packingDestination, 
            string department, string agentName);

        /// <summary>
        /// Generate the commision payment to the agent
        /// Return true if this was success otherwise false.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        bool GenerateCommissionPayment(string agent);
    }
}
