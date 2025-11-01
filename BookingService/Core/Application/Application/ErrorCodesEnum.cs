using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public enum ErrorCodesEnum
{
    //GUEST
    NOT_FOUND = 1,
    COULD_NOT_STORE_DATA = 2,
    INVALID_PERSON_ID = 3,
    INVALID_EMAIL = 4,
    MISSING_REQUIRED_DATA = 5,
    GUEST_NOT_FOUND = 6,
    
    //ROOM
    ROOM_NOT_FOUND = 100,
    ROOM_COULD_NOT_STORE_DATA = 101,
    ROOM_INVALID_PERSON_ID = 102,
    ROOM_MISSING_REQUIRED_INFORMATION = 103,
    ROOM_INVALID_EMAIL = 104,
    ROOM_GUEST_NOT_FOUND = 105,
    ROOM_CAN_NOT_BE_BOOKED = 106,
    
    //BOOKING
    BOOKING_NOT_FOUND = 201,
    BOOKING_COULD_NOT_STORE_DATA = 202,
    BOOKING_INVALID_GUEST = 203,
    BOOKING_INVALID_ROOM = 204,
    BOOKING_MISSING_REQUIRED_INFORMATION = 205,
    
}

