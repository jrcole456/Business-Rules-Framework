using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRulesEngine
{
    /// <summary>
    /// This holds the agent information.
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// The name of the aghent.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This is true if we can generate a commision payment.
        /// </summary>
        public bool CanGenerateCommissionPayment { get; set; }

        /// <summary>
        /// DEfault Comnstructor.
        /// </summary>
        public Agent()
        {
            Name = String.Empty;
            CanGenerateCommissionPayment = false;
        }

        public Agent(Agent other)
        {
            Name = other.Name;
            CanGenerateCommissionPayment = other.CanGenerateCommissionPayment;
        }

        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="name"></param>
        public Agent(string name)
        {
            Name = name;
            CanGenerateCommissionPayment = false;

        }

        public Agent(string agentName, bool canGenerateCommisionPayment)
        {
            Name = agentName;
            CanGenerateCommissionPayment = canGenerateCommisionPayment;
        }
    }
}
