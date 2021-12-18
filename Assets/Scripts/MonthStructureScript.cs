using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton for the month structure
public class MonthStructureScript : MonoBehaviour
{
    private static MonthStructureScript _Instance;

    //Singleton
    public static MonthStructureScript Instance()
    {
        if (_Instance == null)
        {
            _Instance = new MonthStructureScript();
        }

        return _Instance;
    }

    public string getTargetMonthName(int monthNumber)
    {
        //Gets the month's name based on the number

        string name = "";

        switch (monthNumber)
        {
            case 1:
                name = "January";
                break;
            case 2:
                name = "February";
                break;
            case 3:
                name = "March";
                break;
            case 4:
                name = "April";
                break;
            case 5:
                name = "May";
                break;
            case 6:
                name = "June";
                break;
            case 7:
                name = "July";
                break;
            case 8:
                name = "August";
                break;
            case 9:
                name = "September";
                break;
            case 10:
                name = "October";
                break;
            case 11:
                name = "November";
                break;
            case 12:
                name = "December";
                break;
        }
        return name;
    }

    public int getTargetMonthMaxDayNumber(int yearNum, int monthNumber)
    {
        //Gets the month's max number based on the month's number

        int MonthMaxDay = -1;

        switch (monthNumber)
        {
            case 1:
                MonthMaxDay = 31;
                break;
            case 2:
                MonthMaxDay = (yearNum % 4 == 0) ? 29 : 28;
                break;
            case 3:
                MonthMaxDay = 31;
                break;
            case 4:
                MonthMaxDay = 30;
                break;
            case 5:
                MonthMaxDay = 31;
                break;
            case 6:
                MonthMaxDay = 30;
                break;
            case 7:
                MonthMaxDay = 31;
                break;
            case 8:
                MonthMaxDay = 31;
                break;
            case 9:
                MonthMaxDay = 30;
                break;
            case 10:
                MonthMaxDay = 31;
                break;
            case 11:
                MonthMaxDay = 30;
                break;
            case 12:
                MonthMaxDay = 31;
                break;
        }

        return MonthMaxDay;
    }

    public string getDayPostFix(int dayNum)
    {
        //Gets the postfix of the day so that UIs can display the dates correctly

        string dayPostfix = "";

        int lastDayDigit = dayNum % 10;
        int secondDayDigit = (int)(dayNum / 10);

        if (secondDayDigit != 1)
        {
            switch (lastDayDigit)
            {
                case 1:
                    dayPostfix = "st";
                    break;
                case 2:
                    dayPostfix = "nd";
                    break;
                case 3:
                    dayPostfix = "rd";
                    break;
                default:
                    dayPostfix = "th";
                    break;
            }
        }
        else
        {
            dayPostfix = "th";
        }

        return dayPostfix;
    }
}
