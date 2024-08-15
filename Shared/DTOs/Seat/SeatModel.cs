﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Seat
{
    public class SeatModel
    {
        public int Id { get; set; }
        public int ScreeningRoom_Id { get; set; }
        public int ChairType_Id { get; set; }
        public string Row { get; set; }
        public string Number { get; set; }

        // Thêm các thuộc tính này vào SeatModel
        public ScreeningRoomModel ScreeningRoom { get; set; }
        public ChairTypeModel ChairType { get; set; }
    }

    public class ScreeningRoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Numberofseats { get; set; }
        public int NumberOfRegularSeat { get; set; }
        public int NumberOfVIPSeat { get; set; }
        public int NumberOfLoveBoxes { get; set; }
    }

    public class ChairTypeModel //Loại ghế
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
