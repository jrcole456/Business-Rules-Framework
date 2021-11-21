using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRulesEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class PackingSlip
    {
        /// <summary>
        /// the name of the packing slip.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The department name of the packing slip.
        /// </summary>
        public string Department { get; private set; }

        /// <summary>
        /// The final destination of the packing slip.
        /// </summary>
        public string Destination { get; private set; }

        /// <summary>
        /// The source of the packing slip.
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// the agent name.
        /// </summary>
        public Agent CommissionAgent { get; set; }

        /// <summary>
        /// All the additional products added to the payment.
        /// </summary>
        public List<Product> AddedProducts { get; private set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PackingSlip()
        {
            Name = String.Empty;
            Destination = String.Empty;
            Source = String.Empty;
            Department = String.Empty;
            CommissionAgent = new Agent(); ;
            AddedProducts = new List<Product>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="department"></param>
        public PackingSlip(string name, string source, string destination,
            string department, Agent agent)
        {
            Name = name;
            Source = source;
            Destination = destination;
            Department = department;
            AddedProducts = new List<Product>();
            CommissionAgent = new Agent(agent); ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="department"></param>
        /// <param name="product"></param>
        public PackingSlip(string name, string source, string destination,
            string department, Product product)
        {
            Name = name;
            Source = source;
            Destination = destination;
            Department = department;
            CommissionAgent = new Agent(); ;
            AddedProducts = new List<Product>() { product };
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="other"></param>
        public PackingSlip(PackingSlip other)
        {
            Name = other.Name;
            Destination = other.Name;
            Source = other.Source;
            Department = other.Department;
            CommissionAgent = new Agent(); ;
            AddedProducts = new List<Product>(other.AddedProducts);
        }

    }
}
