using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void PlayAudioForRespectiveObject(CharacterEnums objEnum, AudioSource objSound)
    {

        switch (objEnum)
        {
            case CharacterEnums.Person:
                objEnum = CharacterEnums.Person;
                PlaySound( objSound);
                break;

            case CharacterEnums.Bulldozer:
                objEnum = CharacterEnums.Bulldozer;
                PlaySound(objSound);
                break;

            case CharacterEnums.Car:
                objEnum = CharacterEnums.Car;
                PlaySound( objSound);
                break;

            default:
                PlaySound( objSound);
                break;
        }
    }


    private void PlaySound(AudioSource source)
    {
        if(source != null && !source.isPlaying)
        {
            source.Play();
        }
    }
}
