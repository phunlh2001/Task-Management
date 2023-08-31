import { bindActionCreators } from '@reduxjs/toolkit'
import { useAppDispatch } from 'app/hooks'

export const useActions = (actionCreators: any) => {
  const dispatch = useAppDispatch()
  return bindActionCreators(actionCreators, dispatch)
}