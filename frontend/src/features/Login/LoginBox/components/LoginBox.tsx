import React, { useEffect, useState } from 'react'
import { ToastContainer, toast } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";
import "./LoginBox.css"

type Props = {
    setShowRegister: (val: boolean) => void
    setToken: (val: boolean) => void
}

const LoginBox: React.FC<Props> = ({ setShowRegister, setToken }) => {

    const [userName, setUserName] = useState<string>()
    const [password, setPassword] = useState<string>()


    const displayLoginNotification = () => {
        toast.success("LoggedIn Successful");
    };

    const handleSubmit = (e: any) => {
        e.preventDefault()
        if (userName?.length == 0 || password?.length === undefined) {
            displayLoginNotification()
            return
        }
        if(userName?.length != 0 && password?.length !== undefined){
            return
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