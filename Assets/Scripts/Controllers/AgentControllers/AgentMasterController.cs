using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AgentMasterController : MonoBehaviour
{
    public List<AgentController> Agents { get; set; }
    public List<AgentController> ActiveAgents { get { return Agents.Where(a => a.gameObject.activeInHierarchy).ToList(); } }

    private void Awake()
    {
        Agents = new List<AgentController>();
    }
}
