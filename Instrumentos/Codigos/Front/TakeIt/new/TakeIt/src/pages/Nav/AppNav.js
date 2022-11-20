import React, { useContext } from 'react';
import { StatusBar } from 'react-native';
import { NavigationContainer } from '@react-navigation/native';
import Routes from '../../routes'

export default function AppNav() {  
  return (
    <NavigationContainer>
      <StatusBar backgroundColor="#7ED957" barStyle="light-content" />
      <Routes/>
    </NavigationContainer>
  ); 
}
