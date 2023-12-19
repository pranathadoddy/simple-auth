import React, { useState } from 'react'
import axios from "axios";
import { useNavigate } from "react-router-dom";

import './LoginSignup.css';

import user_icon from '../Assets/person.png';
import email_icon from '../Assets/email.png';
import password_icon from '../Assets/password.png';


const LoginSignup = () => {

  const [action, setAction] = useState("Sign In");
  const navigate = useNavigate();
  const [userName, setUserName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  function handleLogin(event) {
    event.preventDefault();

    const loginPayload = {
      email: email,
      password: password,
    };

    axios
      .post("https://localhost:7093/api/users/login", loginPayload)
      .then((response) => {
        const token = response.data.token;

        localStorage.setItem("token", token);

        if (token) {
          axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
        }

        navigate("/Home");
      })
      .catch((err) => console.log(err));
  }

  function handleRegistration(event) {
    event.preventDefault();

    const registrationPayload = {
      email: email,
      password: password,
      userName: userName,
      role: "User"
    };

    axios
      .post("https://localhost:7093/api/users/register", registrationPayload)
      .then((response) => {
         setAction("Sign In");
      })
      .catch((err) => console.log(err));
  }

   const changeAction = (e) =>
   {
        setAction(action === "Sign In" ? "Sign Up": "Sign In") 
   }

   function handleEmailChange(event) {
     setEmail(event.target.value);
   }

   function handleUserNameChange(event) {
    setUserName(event.target.value);
  }

  function handlePasswordChange(event) {
    setPassword(event.target.value);
  }

  return (
    <div className='container'>
        <div className='header'>
            <div className='text'>{action === "Sign In" ? "Sign In": "Sign Up"}</div>
            <div className='underlined'></div>
        </div>
        <div className='inputs'>
            {
                action === "Sign Up"? (
                <div className='input'>
                    <img src={user_icon} alt=''></img>
                    <input type='text' onChange={handleUserNameChange} placeholder='UserName' />
                </div>
                ):(<div></div>)
            }
            <div className='input'>
                <img src={email_icon} alt=''></img>
                <input type='text' onChange={handleEmailChange} placeholder='Email' />
            </div>
            <div className='input'>
                <img src={password_icon} alt=''></img>
                <input type='password' onChange={handlePasswordChange} placeholder='Password' />
            </div>
        </div>
        <a className='signup-link' onClick={changeAction}  href='#'>{ action === "Sign In" ? "Sign Up": "Sign In" }</a>
        <div className='submit-container'>
            {
                action === "Sign In" ? (<div className='submit' onClick={handleLogin}>Login</div>):(<div className='submit' onClick={handleRegistration}>Sign Up</div>)
            }
        </div>
    </div>
  )
}

export default LoginSignup
