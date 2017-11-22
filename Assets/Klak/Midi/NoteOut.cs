﻿using UnityEngine;
using Klak.Math;
using Klak.Wiring;
using MidiJack;

namespace Klak.Midi
{
    [AddComponentMenu("Klak/Wiring/Output/MIDI/Note Out")]
    public class NoteOut : NodeBase
    {
        #region Editable properties

        [SerializeField]
        MidiDestination _destination;

        [SerializeField]
        MidiChannel _channel = MidiChannel.All;

        #endregion

        #region Private members

        int _noteNumber;
        int _velocity;

        #endregion

        #region Node I/O

        [Inlet]
        public float noteNumber {
            set {
                _noteNumber = (int)value;
            }
        }

        [Inlet]
        public float velocity {
            set {
                _velocity = (int)value;
            }
        }

        [Inlet]
        public void NoteOn()
        {
            _destination.SendKeyDown(_channel, _noteNumber, _velocity);
        }

        [Inlet]
        public void NoteOff()
        {
            _destination.SendKeyUp(_channel, _noteNumber);
        }

        #endregion

        #region MonoBehaviour functions

        void OnEnable()
        {
            if (_destination == null)
                _destination = MidiMaster.GetDestination();
        }

        #endregion
    }
}