import { View, Text, StyleSheet, TextInput, TouchableOpacity } from 'react-native'
import React, { useContext, useState } from 'react'

import * as Animatable from 'react-native-animatable'
import { AuthContext } from '../../context/AuthContext'
import { useNavigation } from '@react-navigation/native'

import Loading from '../../components/loading'

export default function SignIn() {
  const navigation = useNavigation();

  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const {login} = useContext(AuthContext);
  
  return (
    <View style={styles.container}>
      <Loading />

      <View style={styles.containerLogo}>
        <Animatable.Image
          animation="fadeIn"
          delay={500}
          source={require('../../assets/logo-pink.png')}
          style={{ width: '50%', top: '35%' }}
          resizeMode="contain"
        />
      </View>

      <Animatable.View animation="fadeInUp" style={styles.containerForm}>
        <TextInput
          placeholder='Usuário'
          style={styles.input}
          autoComplete='username'
          value={username}
          onChangeText={text => { setUsername(text) }}
        />

        <TextInput
          placeholder='Senha'
          style={styles.input}
          autoComplete='password'
          secureTextEntry={true}
          value={password}
          onChangeText={text => { setPassword(text) }}
        />

        <TouchableOpacity 
          disabled={!username || !password}
          style={styles.button}
          onPress={ async () => {
            // await login(username, password, () => navigation.navigate('Events'));
            await login(username, password, (token) => token.role == 'Customer' ? navigation.navigate('Tickets') : navigation.navigate('Gatekeeper'));
          } }
        >
          <Text style={styles.buttonText}>Acessar</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.buttonRegister}>
          <Text style={styles.buttonRegisterText}>Primeiro acesso? Cadastre-se</Text>
        </TouchableOpacity>

      </Animatable.View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
  },
  containerLogo: {
    flex: 2,
    justifyContent: 'center',
    alignItems: 'center'
  },
  containerForm: {
    flex: 3,
    alignSelf: 'center'
  },
  input: {
    backgroundColor: '#f5f5f5',
    border: 1,
    borderRadius: 15,
    height: 58,
    width: 315,
    marginTop: 10,
    marginBottom: 10,
    paddingLeft: 15,
    fontSize: 18
  },
  button: {
    backgroundColor: '#7ED957',
    borderRadius: 15,
    alignSelf: 'center',
    height: 50,
    width: 250,
    marginTop: 10,
    alignItems: 'center',
    justifyContent: 'center'
  },
  buttonText: {
    fontSize: 18,
    color: '#fff',
    fontWeight: 'bold'
  },
  buttonRegister: {
    marginTop: 50,
    alignItems: 'center',
    justifyContent: 'center'
  },
  buttonRegisterText: {
    color: '#d957d0',
  }
})