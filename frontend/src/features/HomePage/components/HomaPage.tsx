import React from 'react'
import './HomaPage.css'

type Props = {}

const HomaPage = (props: Props) => {

  const handleCreateTB = ()=>{
    console.log("ahsdghua");
  }

  

  return (
    <div className='homepage'>
      <button onClick={handleCreateTB}>Create</button>
    </div>
  )
}

export default HomaPage