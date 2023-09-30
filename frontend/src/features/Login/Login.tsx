import React, { useState } from 'react';
import "./Login.css";
import LoginBox from './LoginBox/components/LoginBox';
import Register from './Register/components/Register';
import { useActions } from 'hooks/useActions';
import { authAction } from './authSlice';
import { Account } from 'models';
import AuthContext, { AuthContextProvider } from './AuthContext';

type Props = {
};

const Login: React.FC<Props> = ({}) => {
  
  const { login } = useActions(authAction)
  const [username, setUsername] = useState<string>('')
  const [password, setPassword] = useState<string>('')

  const[showRegister, setShowRegister] = useState(false)

  const handleLogin = async (value: Account) => {
    login(value)
  }
  
  return (
    <AuthContextProvider>
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
            {showRegister?<Register setShowRegister={setShowRegister}/>:<LoginBox setShowRegister={setShowRegister} />}
        </div>    
    </div>
    </AuthContextProvider>
    
  ) 
}


export default Login

