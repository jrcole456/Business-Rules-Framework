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
    public class Product
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
    /// Book class.
    /// </summary>
    public class Book : Product
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Book() : base()
        {
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

    /// <summary>
    /// The base payment class for all Physical Products.
    /// </summary>
    public class PhysicalProductPayment : IPayment
    {
        /// <summary>
        /// All the packing slips for this physical product.
        /// </summary>
        public List<PackingSlip> PackingSlips { protected set; get; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PhysicalProductPayment()
        {
            PackingSlips = new List<PackingSlip>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentName"></param>
        /// <returns></returns>
        public virtual bool GenerateCommissionPayment(string agentName)
        {
            bool isSuccess = true;
            // Can we generate the commision payment.                                      
            // Generate the commision payment.
            
            return isSuccess;
        }

        /// <summary>
        /// Generate the packing slip.
        /// </summary>
        /// <returns></returns>
        public virtual bool GeneratePackingSlip(string packingName, string packingSource, string packingDestination, 
            string department, string agentName)
        {
            PackingSlip packingSlip = new PackingSlip(packingName, packingSource, packingDestination,                 
                department, new Agent(agentName, true));
            // The name and desitnation are mandotary - MUST be defined.
            if (!packingSlip.IsValid())
            {
                Console.WriteLine(String.Format("{0} : PACKING SLIP NAME / DESTINATION / DEPARTMENT NOT DEFINED", 
                    DateTime.UtcNow));
                return false;
            }

            PackingSlips.Add(packingSlip);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packingName"></param>
        /// <param name="packingSource"></param>
        /// <param name="packingDestination"></param>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public virtual bool MakePayment(string packingName, string packingSource, string packingDestination, 
            string department, string agentName)
        {
            bool isSuccess = true;
            if (!GeneratePackingSlip(packingName, packingSource, packingDestination, 
                department, agentName))
            {
                isSuccess = false;
                Console.WriteLine(String.Format("{0} : ERROR : FAILED TO GENERATE PACKING SLIP", DateTime.UtcNow));
            }
            else
            {
                isSuccess = GenerateCommissionPayment(agentName);
            }

            return isSuccess;
        }
    }

    /// <summary>
    /// Holds the Physical Product - BOOK.
    /// </summary>
    public class BookPayment : PhysicalProductPayment
    {
        /// <summary>
        /// For the Physical Product which is a BOOK - Create duplicate packing slips.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public override bool GeneratePackingSlip(string name, string source, string destination, 
            string department, string agentName)
        {
            PackingSlip packingSlip = new PackingSlip(name, source, destination, 
                "royalty", new Agent(agentName, true));

            PackingSlips.Add(packingSlip);
            PackingSlips.Add(packingSlip);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public override bool MakePayment(string name, string source, string destination, 
            string department, string agentName)
        {
            bool isSuccess = true;
            Console.WriteLine(String.Format("{0} : MakePayment for BOOK", DateTime.UtcNow));
            if (!GeneratePackingSlip(name, source, destination, department, agentName))
            {
                Console.WriteLine(String.Format("{0} : ERROR : in Make Payment for Book", DateTime.UtcNow));
                isSuccess = false;
            }
            else
            {
                isSuccess = GenerateCommissionPayment(agentName);
            }

            return isSuccess;
        }
    }
}
