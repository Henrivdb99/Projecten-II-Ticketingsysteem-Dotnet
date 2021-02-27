﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public class Ticket
    {
        private string _titel;
        private DateTime _datumAanmaken;
        private string _omschrijving;
        private int _typeTicket;

        public string Titel
        {
            get => _titel;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ticket moet een naam hebben");
                _titel = value;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ticketid { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime DatumAanmaken {
            get => _datumAanmaken; 
            set
            {
                if (value == null)
                    throw new ArgumentException("Ticket moet een datum bevatten");
                _datumAanmaken = value;
            }
        }
        public string Omschrijving
        {
            get => _omschrijving;
            set
            {
                if (value == null)
                    throw new ArgumentException("Ticket moet een omschrijving bevatten");
                _omschrijving = value;
            }
        }
        public int TypeTicket
        {
            get => _typeTicket;
            set
            {
                if (value == null)
                    throw new ArgumentException("Ticket moet een type bevatten");
                _typeTicket = value;
            }
        }
        public string KlantId { get; set; }
        public string Technieker { get; set; }
        public string Opmerkingen { get; set; }
        public string Bijlage { get; set; }

        public Ticket()
        {

        }
        public Ticket(string titel, int ticketId, TicketStatus ticketStatus, DateTime date, string omschrijving, string typeTicket, string klantId, string technieker ="Geen technieker", string opmerkingen = "Geen opmerkingen", string bijlage = "Geen Bijlage")
        {
            Titel = titel;
            Ticketid = ticketId;
            Status = ticketStatus;
            DatumAanmaken = date;
            Omschrijving = omschrijving;
            TypeTicket = Int32.Parse(typeTicket);
            KlantId = klantId;
            Technieker = technieker;
            Opmerkingen = opmerkingen;
            Bijlage = bijlage;
        }

        //Nice to have
        //public int Waardering { get; set; }
        //public bool ViaKnowledgebase { get; set; }
        //public bool SupportNodig { get; set; }
    }

}
