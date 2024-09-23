using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterTrainerDTO : OutDTO
{
    private string email;
   
    private string passTest;

    private string nationality;

    private string username;

    private string firstname;

    private string lastname;

    private string dni;

    private string phone;

    private string sex;

    private string icon;

    private string avatar;
    /*
    public RegisterTrainerDTO(string email, string passTest, string nationality, string username, string firstname, string lastname, string dni, string phone, string sex, string icon, string avatar)
    {
        this.email = email;
        this.passTest = passTest;
        this.nationality = nationality;
        this.username = username;
        this.firstname = firstname;
        this.lastname = lastname;
        this.dni = dni;
        this.phone = phone;
        this.sex = sex;
        this.icon = icon;
        this.avatar = avatar;
    }
    */
    /*
    public RegisterTrainerDTO()
    {
        this.email = "";
        this.passTest = "";
        this.nationality = "";
        this.username = "";
        this.firstname = "";
        this.lastname = "";
        this.dni = "";
        this.phone = "";
        this.sex = "X";
        this.icon = "";
        this.avatar = "1";
    }*/

    public RegisterTrainerDTO(Dictionary<string, string> info) : base(info)
    {

    }

    public string Email { get => email; set => email = value; }
    public string PassTest { get => passTest; set => passTest = value; }
    public string Nationality { get => nationality; set => nationality = value; }
    public string Username { get => username; set => username = value; }
    public string Firstname { get => firstname; set => firstname = value; }
    public string Lastname { get => lastname; set => lastname = value; }
    public string Dni { get => dni; set => dni = value; }
    public string Phone { get => phone; set => phone = value; }
    public string Sex { get => sex; set => sex = value; }
    public string Icon { get => icon; set => icon = value; }
    public string Avatar { get => avatar; set => avatar = value; }

    public override WWWForm ToForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("passTest", passTest);
        form.AddField("nationality", nationality);
        form.AddField("username", username);
        form.AddField("firstname", firstname);
        form.AddField("lastname", lastname);
        form.AddField("dni", dni);
        form.AddField("sex", sex);
        form.AddField("icon", icon);
        form.AddField("avatar", avatar);
        return form;
    }
}
