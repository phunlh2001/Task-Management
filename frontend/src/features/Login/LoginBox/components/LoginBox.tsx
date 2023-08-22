import React, { useEffect, useState } from 'react'
import "./LoginBox.css"
import { boolean } from 'yup'

type Props = {
    setShowRegister: (val: boolean) => void
    setToken: (val: boolean) => void
}

const LoginBox: React.FC<Props> = ({ setShowRegister, setToken }) => {

    const [UserName, setUserName] = useState<string>()
    const [password, setPassword] = useState<string>()
    const [toast, setToast] = useState<Boolean>()

    return (
        <div className='login__signin'>
            <div className="box-show">
                {toast ? "" : <div className="ntf-box warning-ntfc ">
                    <p>The pass word or the username cant be empty</p>
                    <hr />
                </div>}
            </div>
            <h1>Time for Login</h1>
            <form action='' className="form_login" >
                <span className='login__username'>
                    <input type="text" placeholder='UserName' onChange={e => setUserName(e.target.value)} />
                </span>
                <span className='login__password'>
                    <input type="password" placeholder='Password' onChange={e => setPassword(e.target.value)} />
                </span>
                <div className="btn_loginspace">
                    <button className='btn_login' type='submit'>Login</button>
                    <button className='btn_regis' type='button'>Register</button>
                </div>
            </form>

        </div>
    )
}

export default LoginBox