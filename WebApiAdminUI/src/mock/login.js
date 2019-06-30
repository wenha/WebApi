import { getParams } from '@/libs/util'
const USER_MAP = {
  super_admin: {
    name: 'super_admin',
    code: '1',
    access: ['super_admin', 'admin'],
    token: 'super_admin',
    avator: 'https://file.iviewui.com/dist/a0e88e83800f138b94d2414621bd9704.png'
  },
  admin: {
    name: 'admin',
    code: '2',
    access: ['admin'],
    token: 'admin',
    avator: 'https://avatars0.githubusercontent.com/u/20942571?s=460&v=4'
  }
}

export const login = req => {
  req = JSON.parse(req.body)
  return {
    data: { token: req.userName },
    success: true,
    status: 'OK'
  }
}

export const getUserInfo = req => {
  const params = getParams(req.url)
  // return USER_MAP[params.token]
  return {
    data: USER_MAP[params.token] || {
      name: params.token,
      code: '99',
      access: ['admin'],
      token: params.token,
      avator: 'https://avatars0.githubusercontent.com/u/20942571?s=460&v=4'
    },
    success: true,
    status: 'OK'
  }
}

export const logout = req => {
  return null
}
