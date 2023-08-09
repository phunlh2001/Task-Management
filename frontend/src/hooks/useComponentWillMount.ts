import { useEffect, useState } from 'react';

function useComponentWillMount(callback: any): void {
  const [mounted, setMounted] = useState(false)
  if (!mounted) callback()

  useEffect(() => setMounted(true), [])
}

export default useComponentWillMount