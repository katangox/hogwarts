using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : NPC {

    private void OnTriggerEnter(Collider collider) {
        Menu.Instance.showPanel("TalkPanel", false).GetComponent<TalkPanel>().showNPCText(Id);
    }
}
