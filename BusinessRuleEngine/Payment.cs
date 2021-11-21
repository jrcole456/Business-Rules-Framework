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
    /// Videop product class.
    /// </summary>
    public class Video : Product
    {
        /// <summary>
        /// EDfault constructor.
        /// By default the video name is First Aid.
        /// </summary>
        public Video()
        {
            Name = String.Empty;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        public Video(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// The Owner details.
    /// </summary>
    public class Owner
    {
        /// <summary>
        /// The owner name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The owner email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// True if this is owner is a member
        /// </summary>
        public bool IsMember { get; set; }

        /// <summary>
        /// True if this member is a upgraded member.
        /// </summary>
        public bool IsUpgraded { get; set; }

        /// <summary>
        /// By default this owner is NOT a member.
        /// </summary>
        public static readonly bool defaultIsMember = false;

        /// <summary>
        /// By default this owner is NOT upgraded.
        /// </summary>
        public static readonly bool defaultIsUpgraded = false;

        /// <summary>
        /// The string representation of the owner.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        { 	
            return String.Format("{0} : {1}", Name, Email);
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Owner()
        {
            Name       = String.Empty;
            Email      = String.Empty;
            IsMember   = defaultIsMember;
            IsUpgraded = defaultIsUpgraded;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public Owner(string name, string email)
        {
            Name = name;
            Email = email;
            IsMember = defaultIsMember;
            IsUpgraded = defaultIsUpgraded;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="isMember"></param>
        /// <param name="isUpgraded"></param>
        public Owner(string name, string email, bool isMember, bool isUpgraded)
        {
            Name       = name;
            Email      = email;
            IsMember   = isMember;
            IsUpgraded = isUpgraded;
        }

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="other"></param>
        public Owner(Owner other)
        {
            Name       = other.Name;
            Email      = other.Email;
            IsMember   = other.IsMember;
            IsUpgraded = other.IsUpgraded;
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

    /// <summary>
    /// This is the membership payment.
    /// </summary>
    public class MembershipPayment : IPayment
    {
        /// <summary>
        /// Holds all the owners.
        /// </summary>
        public List<Owner> Owners { get; private set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public MembershipPayment()
        {
            Owners = new List<Owner>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentName"></param>
        /// <returns></returns>
        public virtual bool GenerateCommissionPayment(string agentName)
        {
            return true;
        }

        /// <summary>
        /// implement and allow to be overriden by inherited classes.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="department"></param>
        /// <param name="agentName"></param>
        /// <returns></returns>
        public virtual bool GeneratePackingSlip(string name, string source, string destination,
            string department, string agentName)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="department"></param>
        /// <param name="agentName"></param>
        /// <returns></returns>
        public virtual bool MakePayment(string name, string source, string destination,
            string department, string agentName)
        {
            return true;
        }

        /// <summary>
        /// This should perform the email to the owner.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual bool EmailOwner(string email)
        {
            List<Owner> owners = GetOwners(email);
            if (owners.Any())
            {
                Console.WriteLine(String.Format("{0} : EMAIL OWNER : {1}",
                    DateTime.UtcNow, email));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns TRUE for success.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool ActivateMembership(string ownerName, string ownerEmail)
        {            
            Owner owner = new Owner(ownerName, ownerEmail, !Owner.defaultIsMember, Owner.defaultIsUpgraded);
            Owners.Add(owner);

            return EmailOwner(ownerEmail);
        }

        /// <summary>
        /// Returns all the owners with this email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        protected List<Owner> GetOwners(string email)
        {
            return Owners.Where(ow => String.Equals(ow.Email, email, 
                StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        /// <summary>
        /// MUST BE A MEMBER to return SUCCESS,
        /// Email should be unique - there may be duplicatge owner names.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual bool UpgradeMembership(string email)
        {       
            bool success = false;
            try
            {          
                List<Owner> owners = new List<Owner>(Owners);
                if (Owners.Any(ow => String.Equals(              
                    ow.Email, email, StringComparison.InvariantCultureIgnoreCase)))           
                {              
                    List<Owner> foundOwners = GetOwners(email);
                    foundOwners.ForEach(ow => {
                    
                            Owner owner = new Owner(ow);
                            if (owners.Remove(ow))
                            {
                                owner.IsUpgraded = !Owner.defaultIsUpgraded;
                                success = true;
                            }
                            else
                            {
                                Console.WriteLine(String.Format("{0} : FAILED TO UPGRADE : {1}", 
                                    DateTime.UtcNow, ow.ToString()));     
                            }
                    
                            owners.Add(owner);                    
                        }); 

                    Owners = new List<Owner>(owners);                          
                }            
                else            
                {              
                    Console.WriteLine(String.Format("{} : THIS OWNER IS NOT ALREADY A MEMBER - WE CANNOT UPGRADE", DateTime.UtcNow));                      
                }            
            }
            catch(Exception ex)
            {
                Console.WriteLine(String.Format("{0} : ERROR : {1}", DateTime.UtcNow, ex.Message));
                success = false;
            }
            finally
            {
                if (success)
                {
                    // Email the owner here.
                    EmailOwner(email);
                }
            }

            return success;
        }
    }
}
