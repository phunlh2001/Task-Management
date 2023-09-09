import React, { useEffect, useReducer, useState } from 'react'
import './HomaPage.css'
import Task from 'features/Task/components/Task'
import axios from 'axios'

type Props = {}



const HomaPage = (props: Props) => {

  const[showBox, setShowBox] = useState<boolean>(false)
  const[offCreate, setOffCreate] = useState<boolean>(true)
  const[FTask, setFtask] = useState<string>('')
  const[TaskArr, setTaskArr] = useState<string[]>([])
  const[showTask, setShowTask] = useState<boolean>(false)

  const accessToken = sessionStorage.getItem("token");


  const handleCreateTB = ()=>{
    setShowBox(showBox => !showBox)
    setOffCreate(false)
  }


  const handleAddNewTask = () =>{
    if (FTask === '') {
      return
    }
    setTaskArr([...TaskArr, FTask])
    setShowBox(showBox => !showBox)
    setShowTask(!showTask)
  }

  useEffect(() => {
  axios.get('http://localhost:5000/api/lists/getAll')
  .then(rsp =>{
    console.log(rsp.data);
  })
  },[])
  

  return (
    <div className='homepage'>
      {offCreate && <button className='btn_createnew' onClick={handleCreateTB}>Create</button>}
      {showBox && <div className="first_task"> 
      <h1 className='first_task__title'>Look like you dont have any task</h1>
        <div className="first_task__input">
          <input type="text" placeholder='Enter your first task here' onChange={e => setFtask(e.target.value)}/>
        </div>
        <div className="first_task__btn">
          <button onClick={handleAddNewTask}>Add</button>
        </div>
      </div>}
      {showTask && <Task TaskArr={TaskArr}/>}
    </div>
  )
}

export default HomaPage