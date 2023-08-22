import React, { useEffect, useRef, useState } from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import {faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons'
import './Register.css'
// interface sapce
type Props = {
  setShowRegister:(val:boolean) => void
}

const Register:React.FC<Props> = ({setShowRegister}) => {

  // State space
  const [userName, setuserName] = useState("")
  const [nameVld, setNameVld] = useState("")
  const [pass, setPass] = useState("")
  const[passVld, setPassVld] = useState("")
  const [repass, setRepass] = useState("")
  const [repassVld, setRepassVld] = useState("")
  const [showPass, setShowPass] = useState(false)
  const [showRePass, setShowRePass] = useState(false)


  // KHUC VUC KHAI BAO BIEN =====================================================================================================
  const eyeIconPass = <FontAwesomeIcon icon={faEye} className='register__eye eye' onClick={e => setShowPass(!showPass)}/>
  const eyeIconRePass = <FontAwesomeIcon icon={faEye} className='register__eye eye' onClick={e => setShowRePass(!showRePass)}/>
  const eyeSlashPass = <FontAwesomeIcon icon={faEyeSlash} className='register__eye eyeslash' onClick={e => setShowPass(!showPass)}/>
  const eyeSlashRePass = <FontAwesomeIcon icon={faEyeSlash} className='register__eye eyeslash' onClick={e => setShowRePass(!showRePass)}/>

  // ===============================================  HANDLE SPACE  =========================================================

  // Ham check cac chu viet hoa tra ve true
  const containsUppercase = (str: string) => {
    return /[A-Z]/.test(str);
  }

  // Ham check ky tu dac biet
  const checkSpecialSymbol = (str:string) =>{
   return  /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/.test(str)
  }

  // ham check space
  const checkSpace = (str:string)=>{
    return /[ ]/.test(str);
  }

  // Ham check username
  const userNameValidate = ()=>{
    if (containsUppercase(userName) || checkSpecialSymbol(userName)) {
      setNameVld("Cant have uppercase or special character")
    }
    if (!containsUppercase(userName) && !checkSpecialSymbol(userName)) {
      setNameVld("")
    }
  }

  // Ham check password
  const passValida = ()=>{
    if (pass.length < 6 || checkSpace(pass)) {
      setPassVld("Pass must larger than 6 and not contain space")
    }
    else setPassVld("")
  }

  // ham check repass
  const rePassValida = ()=>{
    if (!(pass == repass)) {
      setRepassVld("PassWord must be same")
    }
    else setRepassVld(""); return;
    
  }

  // Su dung useeffect thong qua moi lan typing se check ra duoc valida cua input nhap duoc 
  useEffect(() => {
    userNameValidate()
    passValida()
    rePassValida()
  }, [userName, pass, repass])


  // ham chua condition check sau khi click button register
  const validateForm = () => {
    if (containsUppercase(userName) || checkSpecialSymbol(userName) || userName.length == 0 || pass.length == 0) {
      alert('pls checking the message')
      return
    }
    else return
  }


  // =============================  RENDERING HERE  ================================================================
  return (
    <div className='register'>
      {/* <div className="box-show">
        {validate?"":<div className="ntf-box warning-ntfc ">
          <p>Can not be empty bro</p>
          <hr />
        </div>}
      </div> */}
      <h1>Enter your infomation</h1>
      <div className="register_box">
        <span><input type="text" placeholder='Username' value={userName} onChange={e => setuserName(e.target.value)} /></span>
        <p className='messagevalid usename'>{nameVld}</p>
        <span><input type={showPass?"text":"password"} placeholder='Password' value={pass} onChange={e => setPass(e.target.value)}/>{showPass?eyeSlashPass:eyeIconPass}</span>
        <p className='messagevalid password'>{passVld}</p>
        <span><input type={showRePass?"text":"password"} placeholder='Repassword' value={repass} onChange={e => setRepass(e.target.value)}/>{showRePass?eyeSlashRePass:eyeIconRePass}</span>
        <p className='messagevalid repassword'>{repassVld}</p>
        <div className="btn_register">
        <button className='register_btn signup' onClick={validateForm}>Sign Up</button>
        <button className='register_btn back_login' onClick={e => setShowRegister(false)}>Back to login</button>
        </div>
      </div>
    </div>
  )
}

export default Register