import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { ToastContainer, toast } from 'react-toastify';
import React, { useEffect, useState } from 'react'
import "react-toastify/dist/ReactToastify.css";
import './Register.css'
import axios from 'axios';
import { error } from 'console';
// interface sapce
type Props = {
  setShowRegister: (val: boolean) => void
}

const Register: React.FC<Props> = ({ setShowRegister }) => {

  // State space
  const [userName, setuserName] = useState("")
  const [nameVld, setNameVld] = useState("")
  const [pass, setPass] = useState("")
  const [passVld, setPassVld] = useState("")
  const [repass, setRepass] = useState("")
  const [repassVld, setRepassVld] = useState("")
  const [showPass, setShowPass] = useState(false)
  const [showRePass, setShowRePass] = useState(false)


  // KHUC VUC KHAI BAO BIEN =====================================================================================================
  const eyeIconPass = <FontAwesomeIcon icon={faEye} className='register__eye eye' onClick={e => setShowPass(!showPass)} />
  const eyeIconRePass = <FontAwesomeIcon icon={faEye} className='register__eye eye' onClick={e => setShowRePass(!showRePass)} />
  const eyeSlashPass = <FontAwesomeIcon icon={faEyeSlash} className='register__eye eyeslash' onClick={e => setShowPass(!showPass)} />
  const eyeSlashRePass = <FontAwesomeIcon icon={faEyeSlash} className='register__eye eyeslash' onClick={e => setShowRePass(!showRePass)} />

  // ===============================================  HANDLE SPACE  =========================================================


  // Ham check cac chu viet hoa tra ve true
  const containsUppercase = (str: string) => {
    return /[A-Z]/.test(str);
  }

  // Toast bao loi 
  const displayLogSuccess = (text: string) => {
    toast.success(text);
  };

  const displayLogErr = (text: string) => {
    toast.error(text);
  };

  // Ham check ky tu dac biet
  const checkSpecialSymbol = (str: string) => {
    return /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/.test(str)
  }

  // ham check space
  const checkSpace = (str: string) => {
    return /[ ]/.test(str);
  }

  // Ham check username
  const userNameValidate = () => {
    if (containsUppercase(userName) || checkSpecialSymbol(userName)) {
      setNameVld("Cant have uppercase or special character")
      return false
    }
    if (!containsUppercase(userName) && !checkSpecialSymbol(userName)) {
      setNameVld(" ")
      return true
    }
  }

  // Ham check password
  const passValida = () => {
    if (pass.length < 6 || checkSpace(pass)) {
      setPassVld("Pass must larger than 6 and not contain space")
      return false
    }
    else setPassVld(" "); return true
  }

  // ham check repass
  const rePassValida = () => {
    if (!(pass === repass)) {
      setRepassVld("PassWord must be same")
      return false
    }
    else setRepassVld(" "); return true

  }

  // Su dung useeffect thong qua moi lan typing se check ra duoc valida cua input nhap duoc 
  useEffect(() => {
    userNameValidate()
    passValida()
    rePassValida()
  }, [userName, pass, repass])


  // Hàm condition display toast lỗi và success
  const validation = () => {
    if (containsUppercase(userName) || checkSpecialSymbol(userName) || userName.length === 0 || pass.length === 0) {
      displayLogErr('pls enter all infomation')
      console.log(1);
      return false
    }
    if(!userNameValidate() || !passValida() || !rePassValida()) {
      console.log(3);
      displayLogErr('Recheck the red note in register space')
      return false
    }
    if (userNameValidate() && passValida() && rePassValida()) {
      displayLogSuccess("look like ur infomation is success, go to login")
      console.log(2);
      return true
    }
   
  }


  // Hàm handle post lên api
  const handleRst = (e: any) => {
    e.preventDefault()
    if (validation()) {
      console.log('chay api');
      let payload = {
        UserName: userName,
        FullName:'nothing',
        Password:pass,
        ConfirmPassword:repass
    }
    axios.post('http://localhost:5000/api/auth/register', payload)
    .then((r) => {
        console.log(r.data);
        localStorage.setItem('token', r.data.token)
    })
    .catch((e) => {
        if (e.response.data.errors !== undefined) {
            console.log('ko co loi');
        }
    });
    }
  }


  // =============================  RENDERING HERE  ================================================================
  return (
    <div className='register'>
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
      <h1>Enter your infomation</h1>
      <form className="register_box" onSubmit={handleRst}>
        <span><input type="text" placeholder='Username' onChange={e => setuserName(e.target.value)} /></span>
        <p className='messagevalid usename'>{nameVld}</p>
        <span><input type={showPass ? "text" : "password"} placeholder='Password' onChange={e => setPass(e.target.value)} />{showPass ? eyeSlashPass : eyeIconPass}</span>
        <p className='messagevalid password'>{passVld}</p>
        <span><input type={showRePass ? "text" : "password"} placeholder='Repassword'onChange={e => setRepass(e.target.value)} />{showRePass ? eyeSlashRePass : eyeIconRePass}</span>
        <p className='messagevalid repassword'>{repassVld}</p>
        <div className="btn_register">
          <button className='register_btn signup' type='submit' >Sign Up</button>
          <button className='register_btn back_login' onClick={e => setShowRegister(false)}>Back to login</button>
        </div>
      </form>
    </div>
  )
}

export default Register