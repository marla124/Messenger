﻿namespace Messenger.Auth.Models;

public class RegisterModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
}