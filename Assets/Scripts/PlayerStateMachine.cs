﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerStateMachine : MonoBehaviour
{
    public enum PlayerState
    {
          Idle
        , InGroupConverstaion
        , JammingToBand
        , Drinking
    }

//    PlayerState current_state;
    List<PlayerState> state_stack = new List<PlayerState>();
    List<ConversationCircle> current_conversations = new List<ConversationCircle>();

    PlayerState CurrentState { 
        get { return state_stack[state_stack.Count - 1]; }
        set { state_stack[state_stack.Count - 1] = value; }
    }
    PlayerState PrevState { 
        get {
            if(state_stack.Count > 1)
                return state_stack[state_stack.Count - 1];
            else
                return PlayerState.Idle;
        } }

    public void Start ()
    {
        state_stack.Add(PlayerState.Idle);

    }

    public void SetState(PlayerState override_state, bool kill_stack = false)
    {
        if(kill_stack)
        {
            state_stack.Clear();
            state_stack.Add(override_state);
        }

        CurrentState = override_state;
    }

    public void PushState(PlayerState new_state)
    {
        state_stack.Add(new_state);
    }

    public PlayerState PopState()
    {
        if(state_stack.Count > 1)
        {
            var temp = CurrentState;
            state_stack.RemoveAt(state_stack.Count - 1);
            return temp;
        }

        return PlayerState.Idle;
    }

    public void EnterConversation(ConversationCircle convo)
    {
        if(current_conversations.Count == 0)
        {
            PushState(PlayerState.InGroupConverstaion);
        }

        current_conversations.Add(convo);
    }

    public void ExitConversation(ConversationCircle convo)
    {
        current_conversations.Remove(convo);

        if(current_conversations.Count == 0)
        {
            state_stack.RemoveAll(IsConvo);
        }
    }

    //apparently I can't use a lambda
    private static bool IsConvo(PlayerState state)
    {
        return state == PlayerState.InGroupConverstaion;
    }
}
