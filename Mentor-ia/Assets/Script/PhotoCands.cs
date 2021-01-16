using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCands : MonoBehaviour
{
    public Sprite[] photoCands;

    public Sprite GetPhotoCand(string cpf) {
        switch (cpf) {
            case("0"):
                return photoCands[0];
            case ("1"):
                return photoCands[1];
            case ("2"):
                return photoCands[2];
            case ("3"):
                return photoCands[3];
            case ("4"):
                return photoCands[4];
            case ("5"):
                return photoCands[5];
            case ("6"):
                return photoCands[6];
            case ("7"):
                return photoCands[7];
            case ("8"):
                return photoCands[8];
            case ("9"):
                return photoCands[9];
            case ("10"):
                return photoCands[10];
            default:
                return photoCands[0];
        }

    }
    
}
