import React, { useState } from 'react'
import { Routes, Route, Link } from 'react-router-dom';
import "./Login.css"
import LoginBox from './LoginBox/components/LoginBox';
import Register from './Register/components/Register';

type Props = {
  setToken:(val:boolean)=> void
  };

const Login: React.FC<Props> = ({setToken}) => {
  
  const[showRegister, setShowRegister] = useState(false)
  
  return (
    <div className='login_page'>
        <div className="login">
            <div className="login__title">
                <h1>
                    wellcome to my application,
                    this is time for your try
                </h1>
                <div id="canvas"></div>
                <div className="login__title__wavy">
                    <img src="" alt="" />   
                </div>
            </div>
            {showRegister?<Register setShowRegister={setShowRegister}/>:<LoginBox setShowRegister={setShowRegister} setToken={setToken}/>}
        </div>    
    </div>
  ) 
}


export default Login

