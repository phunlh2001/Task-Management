import React, { useEffect, useState } from 'react'
import { ToastContainer, toast } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";
import "./LoginBox.css"
import axios from 'axios';
import { error } from 'console';

type Props = {
    setShowRegister: (val: boolean) => void
    setToken: (val: boolean) => void
}

const LoginBox: React.FC<Props> = ({ setShowRegister, setToken }) => {

//tài khoản admin
// {
//     "UserName": "AdminSystem",
//     "Password": "@123456"
//   }

    const [userName, setUserName] = useState<string>('')
    const [password, setPassword] = useState<string>('')


    const validate = () => {
        if (userName?.length === 0 || password?.length === undefined) {
            toast.warning("cant be empty");
            return false
        }
        if (userName?.length !== 0 && password?.length !== undefined) {
            return true
        }
    }


    const handleSubmit = async (e: any) => {
        e.preventDefault()
        if (validate()) {
           await axios.post('http://localhost:5000/api/auth/login', {
                UserName: userName,
                Password:password
            })
                .then((response) => {
                    if (response.status === 200) {
                            setToken(true)
                            sessionStorage.setItem("jwt", response.data.Data.AccessToken);
                            console.log(response);
                    }
                }).catch(error => {
                    if (error.response.status === 400) {
                        toast.warning("userName or password was wrong");
                    }
                })
        }

    }


    return (
        <div className='login__signin'>
            <ToastContainer
                position='top-right'
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme='light'
            />
            <h1>Time for Login</h1>
            <form action='' className="form_login" onSubmit={handleSubmit}>
                <span className='login__username'>
                    <input type="text" placeholder='UserName' onChange={e => setUserName(e.target.value)} />
                </span>
                <span className='login__password'>
                    <input type="password" placeholder='Password' onChange={e => setPassword(e.target.value)} />
                </span>
                <div className="btn_loginspace">
                    <button className='btn_login' type='submit'>Login</button>
                    <button className='btn_regis' type='button' onClick={e => setShowRegister(true)}>Register</button>
                </div>
            </form>

        </div>
    )
}

export default LoginBox