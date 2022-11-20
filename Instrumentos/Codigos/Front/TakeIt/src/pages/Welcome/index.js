import { View, Text, StyleSheet, Image, TouchableOpacity } from 'react-native'
import React from 'react'

import * as Animatable from 'react-native-animatable'
import { useNavigation } from '@react-navigation/native'
import axios from 'axios';

export default function Welcome() {
  const navigation = useNavigation();

  axios.defaults.baseURL = "http://EC2Co-EcsEl-Z57Y9UO0KY8Y-890817140.us-east-2.elb.amazonaws.com";
  axios.defaults.headers.common['content-type'] = "application/json";

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