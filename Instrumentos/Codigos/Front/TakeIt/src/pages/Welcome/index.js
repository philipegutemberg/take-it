import { View, Text, StyleSheet, Image, TouchableOpacity } from 'react-native'
import React from 'react'

import * as Animatable from 'react-native-animatable'
import { useNavigation } from '@react-navigation/native'

export default function Welcome() {
  const navigation = useNavigation();

  return (
    <View style={styles.container}>
      <View style={styles.containerLogo}>
        <Animatable.Image
          animation="flipInY"
          source={require('../../assets/logo-white.png')}
          style={{ width: '70%' }}
          resizeMode="contain"
          onAnimationEnd={ () => navigation.navigate('SignIn') }
        />
      </View>

      {/* <Animatable.View animation='fadeInUp' delay={600}  style={styles.containerForm}>
        <TouchableOpacity 
          style={styles.button}
          onPress={ () => navigation.navigate('SignIn') }
        >
          <Text style={styles.buttonText}>Acessar</Text>
        </TouchableOpacity>
      </Animatable.View> */}
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#7ED957',
  },
  containerLogo: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center'
  }
})